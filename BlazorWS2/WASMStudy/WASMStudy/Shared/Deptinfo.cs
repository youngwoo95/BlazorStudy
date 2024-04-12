using System;
using System.Collections.Generic;

namespace WASMStudy.Shared;

public partial class Deptinfo
{
    public int Deptid { get; set; }

    public int Compid { get; set; }

    public string Deptname { get; set; } = null!;

    public DateTime? Createdt { get; set; }

    public virtual ICollection<Userinfo> Userinfos { get; set; } = new List<Userinfo>();
}
