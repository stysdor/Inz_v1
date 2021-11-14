using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNTK;
using CNTKUtil;

namespace ML.Model
{
    class LandTrainingEngine : TrainingEngine
    {
        protected override Variable CreateFeatureVariable()
        {
            throw new NotImplementedException();
        }

        protected override Variable CreateLabelVariable()
        {
            throw new NotImplementedException();
        }

        protected override Function CreateModel(Variable features)
        {
            throw new NotImplementedException();
        }
    }
}
