using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using Project_EgennamJO.Core;

namespace Project_EgennamJO.Alogrithm
{
    public enum InspectType
    {
        InspNone = -1,
        InspBinary,
        InspMatch,
        InspFilter,
        InspAIModule,
        InspCount
    }
    public abstract class InspAlgorithm
    {
        public InspectType InspectType { get; set; } = InspectType.InspNone;
        public bool IsUse { get; set; } = true;
        public bool IsInspected { get; set; } = false;

        public Rect TeachRect { get; set; } // TeachRect는 검사 영역을 지정하는데 사용됩니다.
        // 보정값에 의해서 이동된 검사영역을 지칭하는게 아니라 원래의 검사를 하기위한 영역을 TeachRect로 지정한다는 의미.
        public Rect InspRect { get; set; } //InspectRect는 실제 검사를 수행할 영역을 지정합니다.
                                           //-> 검사하고자하는 제품이 움직여도 검사하는 영역을 InpserctRect로 말함.
        public eImageChannel ImageChannel { get; set; } = eImageChannel.Gray;
        protected Mat _srcImage = null;
        public List<string> ResultString { get; set; } = new List<string>();
        public bool IsDefect { get; set; }

        public abstract InspAlgorithm Clone();

        public abstract bool CopyFrom(InspAlgorithm sourceAlog);

        protected void CopyBaseTo(InspAlgorithm target)
        {
            target.InspectType = this.InspectType;
            target.IsUse = this.IsUse;
            target.IsInspected = this.IsInspected;
            target.TeachRect = this.TeachRect;  
            target.InspRect = this.InspRect;
        }
        public virtual void SetInspData(Mat srcImage)
        {
            _srcImage = srcImage;
        }
        public abstract bool DoInspect();
        public virtual void ResetResult()
        {
            IsInspected = false;
            IsDefect = false;
            ResultString.Clear();
        }
        public virtual int GetResultRect(out List<DrawInspectInfo>resultArea)
        {
            resultArea = null;
            return 0;
        }
    }
}
