using System;
using System.Collections.Generic;

namespace ImageCompress.AccountSQL.DBModels.ImageCompress;

public partial class Account
{
    public Guid Id { get; set; }

    public string? Email { get; set; }

    public string? GoogleId { get; set; }

    public string? LineId { get; set; }

    public int? State { get; set; }

    public DateTime? CreateDate { get; set; }

    public Guid? CreateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public Guid? UpdateBy { get; set; }

    public string? Password { get; set; }
}
