﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Itrantion.Server.Models
{
    [Table("texts")]
    public class TextModel
    {
        [Column("id")][Key] public Guid Id { get; set; } = Guid.NewGuid();
        [Column("slide_id")] public Guid SlideId { get; set; }
        [Column("text")] public string Text { get; set; } = string.Empty;
        [Column("top")] public int Top { get; set; }
        [Column("left")] public int Left { get; set; }
        [Column("height")] public int Height { get; set; }
        [Column("width")] public int Width { get; set; }
        [Column("sheet_height")] public int SheetHeight { get; set; }
        [Column("sheet_width")] public int SheetWidth { get; set; }
        [Column("font_family")] public string FontFamily { get; set; } = "Tahoma";
        [Column("font_style")] public string FontStyle { get; set; } = "normal";
        [Column("font_weight")] public string FontWeight { get; set; } = "400";
        [Column("text_align")] public string TextAlign { get; set; } = "left";
        [Column("font_size")] public string FontSize { get; set; } = "62px";
        [Column("text_decoration")] public string TextDecoration { get; set; } = "none";

        [NotMapped] public Guid PresentationId { get; set; }

        public TextModel() { }
    }
}