// refered to:
//     https://github.com/unity3d-jp/MeshSync/blob/9d29838eb21249b3057613e0a0a23042a4bfd4ab/Plugin~/Src/MeshSyncTest/TestMeshUtils.cpp

using UnityEngine;
using Unity.Mathematics;
using NUnit.Framework;
using Random = Unity.Mathematics.Random;

namespace Quaternion32.Editor.Tests
{
    public sealed class Quaternion32Tests
    {
        [Test]
        public void 正しく圧縮できているかチェック()
        {
            const int N = 100;
            //const float EPS = 0.01f;
            const float EPS = 1.0f;

            var quarArr = new Quaternion[N];
            var quat32Arr = new Quaternion32[N];

            var forward = new float3(0.0f, 0.0f, 1.0f);
            var rnd = new Random((uint) System.DateTime.Now.Ticks);
            for (int i = 0; i < N; ++i)
            {
                var axis = rnd.NextFloat3();
                var angle = rnd.NextFloat() * math.PI;

                var orig = quarArr[i] = quaternion.AxisAngle(axis, angle);
                quat32Arr[i] = Quaternion32.Compressed(orig);
                var result = quat32Arr[i].Decompressed();
                var ffa = math.rotate(orig, forward);
                var ffb = math.rotate(result, forward);
                Debug.Log($"ffa:{ffa}\nffb:{ffb}");
                Assert.IsTrue(NearEqual(ffa, ffb, EPS), $"ffa:{ffa}\nffb:{ffb}");
            }
        }

        static bool NearEqual(float a, float b, float epsilon)
            => math.abs(a - b) < epsilon;

        static bool NearEqual(float3 a, float3 b, float e)
            => NearEqual(a.x, b.x, e) && NearEqual(a.y, b.y, e) && NearEqual(a.z, b.z, e);
    }
}
