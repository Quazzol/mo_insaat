using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Misc;

[SuppressMessage("ReSharper", "IdentifierTypo")]
public class AesEncryption
{
    private static readonly byte[] Key =
    {
            123,
            217,
            19,
            11,
            24,
            26,
            85,
            45,
            114,
            184,
            27,
            162,
            37,
            112,
            222,
            209,
            241,
            24,
            175,
            144,
            173,
            53,
            196,
            29,
            24,
            26,
            17,
            218,
            131,
            236,
            53,
            209
        };

    private static readonly byte[] Vector =
    {
            146,
            64,
            191,
            111,
            23,
            3,
            113,
            119,
            231,
            121,
            221,
            112,
            79,
            32,
            114,
            156

        };

    private static readonly ICryptoTransform Encryptor;
    private static readonly ICryptoTransform Decryptor;

    private static readonly UTF8Encoding Encoder;
    static AesEncryption()
    {
        var rm = Aes.Create("AesManaged");
        Encryptor = rm!.CreateEncryptor(Key, Vector);
        Decryptor = rm.CreateDecryptor(Key, Vector);
        Encoder = new UTF8Encoding();
    }

    public static string Encrypt(string? unencrypted)
    {
        if (unencrypted is null)
            return string.Empty;
        return Convert.ToBase64String(Encrypt(Encoder.GetBytes(unencrypted)));
    }

    public static string Decrypt(string encrypted)
    {
        return Encoder.GetString(Decrypt(Convert.FromBase64String(encrypted)));
    }

    public static byte[] Encrypt(byte[] buffer)
    {
        return Transform(buffer, Encryptor);
    }

    public static byte[] Decrypt(byte[] buffer)
    {
        return Transform(buffer, Decryptor);
    }

    private static readonly object LockObject = new object();
    protected static byte[] Transform(byte[] buffer, ICryptoTransform cryptoTransform)
    {
        lock (LockObject)
        {
            var stream = new MemoryStream();
            using (var cs = new CryptoStream(stream, cryptoTransform, CryptoStreamMode.Write))
            {
                cs.Write(buffer, 0, buffer.Length);
            }
            return stream.ToArray();
        }
    }

    public static string EncryptAes256(string text, string password)
    {
        var originalBytes = Encoding.UTF8.GetBytes(text);
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

        const int saltSize = 4;
        var ba = new byte[saltSize];
        RandomNumberGenerator.Create().GetBytes(ba);
        var saltBytes = ba;

        var bytesToBeEncrypted = new byte[saltBytes.Length + (originalBytes.Length - 1) + 1];
        for (var i = 0; i <= saltBytes.Length - 1; i++)
        {
            bytesToBeEncrypted[i] = saltBytes[i];
        }
        for (var i = 0; i <= originalBytes.Length - 1; i++)
        {
            bytesToBeEncrypted[i + saltBytes.Length] = originalBytes[i];
        }

        var encryptedBytes = EncryptAes256(bytesToBeEncrypted, passwordBytes);

        return Convert.ToBase64String(encryptedBytes);
    }

    public static string DecryptAes256(string text, string password)
    {
        var bytesToBeDecrypted = Convert.FromBase64String(text);
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

        var decryptedBytes = DecryptAes256(bytesToBeDecrypted, passwordBytes);

        const int saltSize = 4;

        var originalBytes = new byte[decryptedBytes.Length - saltSize];

        for (var i = saltSize; i <= decryptedBytes.Length - 1; i++)
        {
            originalBytes[i - saltSize] = decryptedBytes[i];
        }

        return Encoding.UTF8.GetString(originalBytes);
    }

    public static byte[] EncryptAes256(byte[] bytesToBeEncrypted, byte[] passwordBytes)
    {
        byte[] encryptedBytes;

        var saltBytes = new byte[]
        {
                8,
                2,
                5,
                4,
                3,
                1,
                7,
                6
        };

        using (var ms = new MemoryStream())
        {
            using (var rijndaelManaged = Aes.Create("AesManaged"))
            {
                rijndaelManaged!.KeySize = 256;
                rijndaelManaged.BlockSize = 128;

                var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                rijndaelManaged.Key = key.GetBytes(Convert.ToInt32(rijndaelManaged.KeySize / 8));
                rijndaelManaged.IV = key.GetBytes(Convert.ToInt32(rijndaelManaged.BlockSize / 8));

                rijndaelManaged.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(ms, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                    cs.Close();
                }
                encryptedBytes = ms.ToArray();
            }
        }

        return encryptedBytes;
    }

    public static byte[] DecryptAes256(byte[] bytesToBeDecrypted, byte[] passwordBytes)
    {
        byte[] decryptedBytes;

        var saltBytes = new byte[]
        {
                8,
                2,
                5,
                4,
                3,
                1,
                7,
                6
        };

        using (var ms = new MemoryStream())
        {
            using (var rijndaelManaged = Aes.Create("AesManaged"))
            {
                rijndaelManaged!.KeySize = 256;
                rijndaelManaged.BlockSize = 128;

                using (var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000))
                {
                    rijndaelManaged.Key = key.GetBytes(Convert.ToInt32(rijndaelManaged.KeySize / 8));
                    rijndaelManaged.IV = key.GetBytes(Convert.ToInt32(rijndaelManaged.BlockSize / 8));
                }

                rijndaelManaged.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(ms, rijndaelManaged.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                    cs.Close();
                }
                decryptedBytes = ms.ToArray();
            }
        }

        return decryptedBytes;
    }
}
