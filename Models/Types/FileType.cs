
namespace WebApi.Models.Types;
public abstract class FileType
{
    protected string Description { get; set; }
    protected string Name { get; set; }

    private List<string> Extensions { get; }
        = new List<string>();

    private List<byte[]> Signatures { get; }
        = new List<byte[]>();
    
    public int SignatureLength => Signatures.Max(m => m.Length);

    protected FileType AddSignatures(params byte[][] bytes)
    {
        Signatures.AddRange(bytes);
        return this;
    }

    protected FileType AddExtensions(params string[] extensions)
    {
        Extensions.AddRange(extensions);
        return this;
    }

    public bool VerifyExtension(string extension)
    {
        return Extensions.Contains(extension);
    }
    
    public bool VerifySignature(Stream stream)
    {
        stream.Position = 0;
        var reader = new BinaryReader(stream);
        var headerBytes = reader.ReadBytes(SignatureLength);

        return Signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature));
            
    }
}
