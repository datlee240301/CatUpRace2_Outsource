// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("7oTNKch8STMRA85+3SiitrS8ngqlhMPob+1MqtTpQq3dADJztpHI0vrcXJ05RcSL35vIBArfJlfBLqfVQXm8L45o3H8nltCW2cNiHACS38jLefrZy/b98tF9s30M9vr6+v77+G1GSs2kO8QLdfWp8QAWUQU17NVL6wA+4FRq68Lgli1kBXCvmVQ9hhWECSlIntqTKr1p7QzYgm1bGJhhsasYteCH9Q5X3Q1x9HmiaXhEKFr7VJduyyHXj8KQmO1qclyh1l2Y3g3PPktmE3NRCphhlySCXmuVjO7KugNhEs0QBoB8M2Gr7s/uJ3P75jTc7FHOw5ubjl6nq/t5H0sEk0PZtv15+vT7y3n68fl5+vr7Rgu70l2c+tQlXscuG4lMxvn4+vv6");
        private static int[] order = new int[] { 11,5,12,7,11,10,9,11,12,11,12,11,13,13,14 };
        private static int key = 251;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
