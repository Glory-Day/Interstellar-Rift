namespace Core.Object.Module
{
    /// <summary>
    /// Specifies the rank of a module, which determines its stats and color.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The ranks are named after the letters of the Greek alphabet and are declared in ascending order,
    /// from <see cref="Alpha"/>, the lowest rank, to <see cref="Lambda"/>, the highest rank.
    /// A higher underlying value therefore indicates a higher rank.
    /// </para>
    /// <para>
    /// Because the underlying values may be serialized, existing members must not be reordered,
    /// and new members must be appended at the end.
    /// </para>
    /// </remarks>
    public enum ModuleRank
    {
        Alpha = 0,
        Beta,
        Gamma,
        Delta,
        Epsilon,
        Zeta,
        Eta,
        Theta,
        Iota,
        Kappa,
        Lambda,
    }
}
