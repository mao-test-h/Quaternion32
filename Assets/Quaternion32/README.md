# 32-bit Compression for quaternions

## Installation

- Edit `Packages/manifest.json`

```json
{
  "dependencies": {
    // (snip)
    "com.mao.quaternion32": "https://github.com/mao-test-h/Quaternion32.git?path=/Assets/Quaternion32",
    // (snip)
  }
}
```



## Usages

```csharp
Vector3 axis;
float angle;
Quaternion orig = quarArr[i] = Quaternion.AngleAxis(angle, axis);

// Compression (Quaternion -> Quaternion32)
Quaternion32 compressed = Quaternion32.Compressed(orig);

// Decompression (Quaternion32 -> Quaternion)
Quaternion decompressed = compressed.Decompressed();
```
