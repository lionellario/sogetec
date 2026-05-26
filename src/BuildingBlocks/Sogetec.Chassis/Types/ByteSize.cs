namespace Sogetec.Chassis.Types;

public readonly struct ByteSize
{
    public long Bytes { get; }

    public ByteSize(long bytes)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(bytes);

        Bytes = bytes;
    }

    public override string ToString() => $"{Bytes}";

    // Helpers
    public double KB => Bytes / 1024d;
    public double MB => Bytes / 1024d / 1024d;
    public double GB => Bytes / 1024d / 1024d / 1024d;
    public double TB => Bytes / 1024d / 1024d / 1024d / 1024d;
    public double PB => Bytes / 1024d / 1024d / 1024d / 1024d / 1024d;
    public double EB => Bytes / 1024d / 1024d / 1024d / 1024d / 1024d / 1024d;

    public static ByteSize FromKB(double kb) => new((long)(kb * 1024));
    public static ByteSize FromMB(double mb) => new((long)(mb * 1024 * 1024));
    public static ByteSize FromGB(double gb) => new((long)(gb * 1024 * 1024 * 1024));
    public static ByteSize FromTB(double tb) => new((long)(tb * 1024 * 1024 * 1024 * 1024));
    public static ByteSize FromPB(double pb) => new((long)(pb * 1024 * 1024 * 1024 * 1024 * 1024));
    public static ByteSize FromEB(double eb) => new((long)(eb * 1024 * 1024 * 1024 * 1024 * 1024 * 1024));
}
