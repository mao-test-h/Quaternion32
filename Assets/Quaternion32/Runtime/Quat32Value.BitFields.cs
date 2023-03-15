// auto-generated
// - Generated from "ufcpp/BitFields"

using System;
using BitFields;

namespace Quaternion32
{
    public partial struct Quaternion32
    {
        public struct Quat32Value
        {
            public uint Value;

            private const int x0Shift = 0;
            private const uint x0Mask = unchecked((uint) ((1U << 10) - (1U << 0)));

            public Bit10 x0
            {
                get => (Bit10) ((Value & x0Mask) >> x0Shift);
                set => Value = unchecked((uint) ((Value & ~x0Mask) | ((((uint) value) << x0Shift) & x0Mask)));
            }

            private const int x1Shift = 10;
            private const uint x1Mask = unchecked((uint) ((1U << 20) - (1U << 10)));

            public Bit10 x1
            {
                get => (Bit10) ((Value & x1Mask) >> x1Shift);
                set => Value = unchecked((uint) ((Value & ~x1Mask) | ((((uint) value) << x1Shift) & x1Mask)));
            }

            private const int x2Shift = 20;
            private const uint x2Mask = unchecked((uint) ((1U << 30) - (1U << 20)));

            public Bit10 x2
            {
                get => (Bit10) ((Value & x2Mask) >> x2Shift);
                set => Value = unchecked((uint) ((Value & ~x2Mask) | ((((uint) value) << x2Shift) & x2Mask)));
            }

            private const int dropShift = 30;
            private const uint dropMask = unchecked((uint) ((1U << 32) - (1U << 30)));

            public Bit2 drop
            {
                get => (Bit2) ((Value & dropMask) >> dropShift);
                set => Value = unchecked((uint) ((Value & ~dropMask) | ((((uint) value) << dropShift) & dropMask)));
            }
        }
    }
}
