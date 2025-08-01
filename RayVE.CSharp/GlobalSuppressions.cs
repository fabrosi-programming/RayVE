// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "No globalization, so no need for text resources.")]
[assembly: SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "No globalization.")]
[assembly: SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Nullable reference types enabled.")]
