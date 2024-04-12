using System;
using System.Collections.Generic;

namespace WASMStudy.Shared;

public partial class Userinfo
{
    public int Reqno { get; set; }

    public string Userid { get; set; } = null!;

    public string Userpw { get; set; } = null!;

    public DateTime? Createdt { get; set; }

    public string? Username { get; set; }

    public string? Phonenumber { get; set; }

    public string? Address { get; set; }

    public int? Compid { get; set; }

    public int? Deptid { get; set; }

    public virtual Compinfo? Comp { get; set; }

    public virtual Deptinfo? Dept { get; set; }
}
