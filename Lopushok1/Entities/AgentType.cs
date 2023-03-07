﻿using System;
using System.Collections.Generic;

namespace Lopushok1.Entities;

public partial class AgentType
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Image { get; set; }

    public virtual ICollection<Agent> Agents { get; } = new List<Agent>();
}
