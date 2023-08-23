using async_stream_poc;

StreamProcessor streamProcessor = new();

_ = Task.Run(streamProcessor.Process);


await streamProcessor.ProduceAsync();


