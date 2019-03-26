using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.AI.MachineLearning;
namespace AcerST_Demo
{
    
    public sealed class BedroomInput
    {
        public TensorFloat img_placeholder00; // shape(1,3,480,640)
    }
    
    public sealed class BedroomOutput
    {
        public TensorFloat add_3700; // shape(1,3,480,640)
    }
    
    public sealed class BedroomModel
    {
        private LearningModel model;
        private LearningModelSession session;
        private LearningModelBinding binding;
        public static async Task<BedroomModel> CreateFromStreamAsync(IRandomAccessStreamReference stream)
        {
            BedroomModel learningModel = new BedroomModel();
            learningModel.model = await LearningModel.LoadFromStreamAsync(stream);
            learningModel.session = new LearningModelSession(learningModel.model);
            learningModel.binding = new LearningModelBinding(learningModel.session);
            return learningModel;
        }
        public async Task<BedroomOutput> EvaluateAsync(BedroomInput input)
        {
            binding.Bind("img_placeholder:0", input.img_placeholder00);
            var result = await session.EvaluateAsync(binding, "0");
            var output = new BedroomOutput();
            output.add_3700 = result.Outputs["add_3700"] as TensorFloat;
            return output;
        }
    }
}
