using System;
using System.Collections.Generic;

namespace WASMStudy.Shared;

public partial class Compinfo
{
    public int Compid { get; set; }

    public string Compname { get; set; } = null!;

    public string? Address { get; set; }

    public string? Tel { get; set; }

    public DateTime? Createdt { get; set; }

    public virtual ICollection<Userinfo> Userinfos { get; set; } = new List<Userinfo>();
}
