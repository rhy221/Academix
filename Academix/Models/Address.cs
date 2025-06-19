using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Academix.Models
{
    public class Address
    {
        public class Province
        {
            public string name { get; set; }
            public string type { get; set; }
            public string slug { get; set; }
            public string name_with_type { get; set; }
            public string code { get; set; }

            [JsonProperty("quan-huyen")]
            public Dictionary<string, District> quan_huyen { get; set; }

            public override string ToString()
            {
                return name;
            }
        }

        public class District
        {
            public string name { get; set; }
            public string type { get; set; }
            public string slug { get; set; }
            public string name_with_type { get; set; }
            public string path { get; set; }
            public string path_with_type { get; set; }
            public string code { get; set; }
            public string parent_code { get; set; }

            [JsonProperty("xa-phuong")]
            public Dictionary<string, Ward> xa_phuong { get; set; }

            public override string ToString()
            {
                return name;
            }
        }

        public class Ward
        {
            public string name { get; set; }
            public string type { get; set; }
            public string slug { get; set; }
            public string name_with_type { get; set; }
            public string path { get; set; }
            public string path_with_type { get; set; }
            public string code { get; set; }
            public string parent_code { get; set; }

            public override string ToString()
            {
                return name;
            }
        }
    }
}
