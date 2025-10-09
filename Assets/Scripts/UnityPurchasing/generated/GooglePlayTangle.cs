// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("s6dob4sIvAcx7uHpojuzj+ZPIwS/VrLRTNFNDyK99r0tNYmB6hZW8flXxpKD2DX+d+o1n0A1qmagtxDOKJoZOigVHhEynlCe7xUZGRkdGBuhLiJ7V2EE7PE8V8RTqx7BeCpHkl4XDe9DBYQsGYjD5MNxGadq3TmqYddnlArncO3qO+mpx/zjZ7FrXHgB4rozOfmlQFtyGLKnD2teRf6RP1hw2mmZ0vQUZleV2NaS54flmVZDUVVnXZ+yq8vcBpGG4E90c/ovG5m+YyksvF2S0NJwwWi2jcsIUkHFiOilKgCkukDMJoNlLOAL6Uel3meyGH5u5bB6xqlBQQU0HV8mSvZ1bO+aGRcYKJoZEhqaGRkYtL04bzIgmCYttBsPcNki3RobGRgZ");
        private static int[] order = new int[] { 3,4,12,3,12,6,11,8,11,9,11,11,13,13,14 };
        private static int key = 24;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
