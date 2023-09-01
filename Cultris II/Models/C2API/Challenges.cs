using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cultris_II.Models.C2API
{
    public class Challenges
    {
        [JsonProperty("ol-cheese")]
        public OlCheese OlCheese { get; set; }

        [JsonProperty("ol-clewett")]
        public OlClewett OlClewett { get; set; }

        [JsonProperty("ol-clewett-old")]
        public OlClewettOld OlClewettOld { get; set; }

        [JsonProperty("ol-maserati")]
        public OlMaserati OlMaserati { get; set; }

        [JsonProperty("ol-qs")]
        public OlQs OlQs { get; set; }

        [JsonProperty("ol-send")]
        public OlSend OlSend { get; set; }

        [JsonProperty("ol-survivor")]
        public OlSurvivor OlSurvivor { get; set; }

        [JsonProperty("ol-ten")]
        public OlTen OlTen { get; set; }

        [JsonProperty("ol-tgm")]
        public OlTgm OlTgm { get; set; }
    }

    public class OlCheese
    {
        [JsonProperty("playDuration")]
        public double PlayDuration { get; set; }

        [JsonProperty("blocks")]
        public int Blocks { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("linesCleared")]
        public int LinesCleared { get; set; }

        [JsonProperty("linesSent")]
        public int LinesSent { get; set; }

        [JsonProperty("tetrii")]
        public int Tetrii { get; set; }
    }

    public class OlClewett
    {
        [JsonProperty("playDuration")]
        public double PlayDuration { get; set; }

        [JsonProperty("blocks")]
        public int Blocks { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("linesCleared")]
        public int LinesCleared { get; set; }

        [JsonProperty("linesSent")]
        public int LinesSent { get; set; }

        [JsonProperty("tetrii")]
        public int Tetrii { get; set; }
    }

    public class OlClewettOld
    {
        [JsonProperty("playDuration")]
        public double PlayDuration { get; set; }

        [JsonProperty("blocks")]
        public int Blocks { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("linesCleared")]
        public int LinesCleared { get; set; }

        [JsonProperty("linesSent")]
        public int LinesSent { get; set; }

        [JsonProperty("tetrii")]
        public double Tetrii { get; set; }
    }

    public class OlMaserati
    {
        [JsonProperty("playDuration")]
        public double PlayDuration { get; set; }

        [JsonProperty("blocks")]
        public int Blocks { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("linesCleared")]
        public int LinesCleared { get; set; }

        [JsonProperty("linesSent")]
        public int LinesSent { get; set; }

        [JsonProperty("tetrii")]
        public int Tetrii { get; set; }
    }

    public class OlQs
    {
        [JsonProperty("playDuration")]
        public double PlayDuration { get; set; }

        [JsonProperty("blocks")]
        public int Blocks { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("linesCleared")]
        public int LinesCleared { get; set; }

        [JsonProperty("linesSent")]
        public int LinesSent { get; set; }

        [JsonProperty("tetrii")]
        public int Tetrii { get; set; }
    }

    public class OlSend
    {
        [JsonProperty("playDuration")]
        public double PlayDuration { get; set; }

        [JsonProperty("blocks")]
        public int Blocks { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("linesCleared")]
        public int LinesCleared { get; set; }

        [JsonProperty("linesSent")]
        public int LinesSent { get; set; }

        [JsonProperty("tetrii")]
        public int Tetrii { get; set; }
    }

    public class OlSurvivor
    {
        [JsonProperty("playDuration")]
        public double PlayDuration { get; set; }

        [JsonProperty("blocks")]
        public int Blocks { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("linesCleared")]
        public int LinesCleared { get; set; }

        [JsonProperty("linesSent")]
        public int LinesSent { get; set; }

        [JsonProperty("tetrii")]
        public int Tetrii { get; set; }
    }

    public class OlTen
    {
        [JsonProperty("playDuration")]
        public double PlayDuration { get; set; }

        [JsonProperty("blocks")]
        public int Blocks { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("linesCleared")]
        public int LinesCleared { get; set; }

        [JsonProperty("linesSent")]
        public int LinesSent { get; set; }

        [JsonProperty("tetrii")]
        public int Tetrii { get; set; }
    }

    public class OlTgm
    {
        [JsonProperty("playDuration")]
        public double PlayDuration { get; set; }

        [JsonProperty("blocks")]
        public int Blocks { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("linesCleared")]
        public int LinesCleared { get; set; }

        [JsonProperty("linesSent")]
        public int LinesSent { get; set; }

        [JsonProperty("tetrii")]
        public int Tetrii { get; set; }
    }
}
