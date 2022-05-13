using System.Collections.Generic;

namespace BMS.Services.Helpers
{
    public class BmsResponse<TData>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public TData Data { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public ResponseCode ResponseCode { get; set; }
    }
}
