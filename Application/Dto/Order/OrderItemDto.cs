﻿namespace Application.Dto.Order;

public record OrderItemDto
{
    public string name { get; set; }
    public string code { get; set; }
    public long count { get; set; }
    public long unit { get; set; }
    public long fee { get; set; }
    public string group { get; set; }
    public string groupTitle { get; set; }
    public string data { get; set; }
}
