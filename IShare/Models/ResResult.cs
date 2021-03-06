using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    /// <summary>
    /// New return format
    /// Status: -1 means fail,1 means success
    /// Msg: success or fail information
    /// ResultData: List that is able to conatin various type return value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResResult<T>
    {
        public int Status { get; set; }
        public List<T> ResultData { get; set; }
        public string Msg { get; set; }
    }
}
