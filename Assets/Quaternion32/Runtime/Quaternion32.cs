// refered to:
//     https://github.com/unity3d-jp/MeshSync/blob/9d29838eb21249b3057613e0a0a23042a4bfd4ab/Plugin~/Src/MeshUtils/muQuat32.h

using UnityEngine;
using Unity.Mathematics;
using BitFields;

namespace Quaternion32
{
    public partial struct Quaternion32
    {
        const float SR2 = 1.41421356237f;
        const float RSR2 = 1.0f / 1.41421356237f;
        const float C = 0x3ff;
        const float R = 1.0f / 0x3ff;

        Quat32Value value;

        public Quaternion32(Quaternion quat) => this = Compressed(quat);
        Quaternion32(Quat32Value quat) => value = quat;

        public Quaternion Decompressed()
        {
            var a0 = unpack(value.x0);
            var a1 = unpack(value.x1);
            var a2 = unpack(value.x2);
            var iss = math.sqrt(1.0f - (square(a0) + square(a1) + square(a2)));

            switch (value.drop)
            {
                case 0:
                    return new Quaternion(iss, a0, a1, a2);
                case 1:
                    return new Quaternion(a0, iss, a1, a2);
                case 2:
                    return new Quaternion(a0, a1, iss, a2);
                default:
                    return new Quaternion(a0, a1, a1, iss);
            }
        }

        public static Quaternion32 Compressed(Quaternion other)
        {
            var v = other;
            float a0, a1, a2;
            var value = new Quat32Value();
            value.drop = (Bit2) dropmax(square(v[0]), square(v[1]), square(v[2]), square(v[3]));
            if (value.drop == 0)
            {
                float s = math.sign(v[0]);
                a0 = v[1] * s;
                a1 = v[2] * s;
                a2 = v[3] * s;
            }
            else if (value.drop == 1)
            {
                float s = math.sign(v[1]);
                a0 = v[0] * s;
                a1 = v[2] * s;
                a2 = v[3] * s;
            }
            else if (value.drop == 2)
            {
                float s = math.sign(v[2]);
                a0 = v[0] * s;
                a1 = v[1] * s;
                a2 = v[3] * s;
            }
            else
            {
                float s = math.sign(v[3]);
                a0 = v[0] * s;
                a1 = v[1] * s;
                a2 = v[2] * s;
            }

            value.x0 = (Bit10) pack(a0);
            value.x1 = (Bit10) pack(a1);
            value.x2 = (Bit10) pack(a2);
            return new Quaternion32(value);
        }

        static uint pack(float a) => (uint) ((a * SR2 + 1.0f) * 0.5f * C);

        static float unpack(uint a) => ((a * R) * 2.0f - 1.0f) * RSR2;

        static float square(float a) => a * a;

        static int dropmax(float a, float b, float c, float d)
        {
            if (a > b && a > c && a > d) return 0;
            if (b > c && b > d) return 1;
            if (c > d) return 2;
            return 3;
        }
    }
}
