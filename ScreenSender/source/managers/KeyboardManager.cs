using ScreenSender.Native;
using System.Collections.Immutable;

namespace ScreenSender.Managers;

public static class KeyboardManager
{
    private const int KeysPollingDelayMs = 250;

    private static IReadOnlyDictionary<Keys, Keys>? _aliases;

    private static bool _isReading;

    public delegate void KeyProvider(Keys key);

    public static event KeyProvider? KeyPressed;

    public class KeysCombination
    {
        private readonly ImmutableArray<Keys> _combination;

        private readonly Action _pressedHandler;

        private Keys _previousKey;

        private int _awaitingIndex;

        public KeysCombination(ImmutableArray<Keys> keys, Action pressedHandler)
        {
            _combination = keys;
            _pressedHandler = pressedHandler;
        }

        public void StartReadingAsync()
        {
            KeyPressed += HandlePressedKey;
        }

        private void HandlePressedKey(Keys pressedKey)
        {
            if (IsMouseKey(pressedKey) || pressedKey == _previousKey)
            {
                return;
            }

            if (pressedKey == _combination[_awaitingIndex])
            {
                _awaitingIndex++;
            }
            else
            {
                _awaitingIndex = 0;
            }

            if (IsPressed())
            {
                _pressedHandler.Invoke();
            }
            else
            {
                _previousKey = pressedKey;
            }
        }

        private static bool IsMouseKey(Keys key)
        {
            return key is Keys.LButton or Keys.RButton or Keys.MButton or Keys.XButton1 or Keys.XButton2;
        }

        private bool IsPressed()
        {
            if (_awaitingIndex == _combination.Length)
            {
                _awaitingIndex = 0;

                return true;
            }

            return false;
        }
    }

    public static void SetAliases(IReadOnlyDictionary<Keys, Keys> aliases)
    {
        _aliases = aliases;
    }

    public static void StartReadKeysAsync()
    {
        if (_isReading)
        {
            throw new InvalidOperationException("keys already being read");
        }
        else
        {
            _isReading = true;
        }

        Task.Run(() =>
        {
            while (true)
            {
                for (var vKey = byte.MinValue; vKey < byte.MaxValue; vKey++)
                {
                    var keyState = NativeMethods.GetAsyncKeyState(vKey);

                    if (IsUserInput(keyState))
                    {
                        var key = (Keys)vKey;

                        if (IsAlias(key, out Keys replaceKey))
                        {
                            key = replaceKey;
                        }

                        KeyPressed?.Invoke(key);
                    }
                }

                Thread.Sleep(KeysPollingDelayMs);
            }
        });
    }

    private static bool IsUserInput(int keyState)
    {
        return keyState is 0x1 or 0x8001;
    }

    private static bool IsAlias(Keys key, out Keys alias)
    {
        if (_aliases is not null && _aliases.TryGetValue(key, out alias))
        {
            return true;
        }

        alias = Keys.None;

        return false;
    }
}
