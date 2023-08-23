using System.Threading.Tasks.Dataflow;

namespace async_stream_poc
{
    public interface IProducer
    {
        Task ProduceAsync();
    }

    public interface IProcessor
    {
        Task Process();
    }
    public class StreamProcessor : IProducer, IProcessor
    {
        private readonly BufferBlock<int> _stream;

        public StreamProcessor()
        {
            _stream = new BufferBlock<int>(
                new DataflowBlockOptions { CancellationToken = CancellationToken.None });
        }

        public async Task Process()
        {
            await foreach(var item in DataflowBlock.ReceiveAllAsync<int>(_stream, CancellationToken.None))
            {
                Console.WriteLine($"Processing {item}");
            }
        }

        public async Task ProduceAsync()
        {
            var random = new Random();
            while (true)
            {
                int value = random.Next(5);
                
                await Task.Delay(value * 100);
                _stream.Post(value);
            }
        }
    }
}