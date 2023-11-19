using HarmonyLib;

namespace COIExtended.Extensions {

    internal class MethodToPatch {
        public HarmonyMethod ToPatch { get; }
        public HarmonyMethod Prefix { get; }
        public HarmonyMethod Postfix { get; }

        public MethodToPatch(HarmonyMethod toPatch, HarmonyMethod prefix, HarmonyMethod postfix) {
            ToPatch = toPatch;
            Prefix = prefix;
            Postfix = postfix;
        }
    }
}