using BAL.Constants;
using BAL.Interfaces;

namespace BAL.Services
{
    public class UniqueIdGenerator : IUniqueIdGenerator
    {
        private static int _sequenceCounter = 0;
        private static readonly object _lock = new object();
        private readonly int _instanceId;
        public UniqueIdGenerator(int instanceId)
        {
            if (instanceId < ApiConstant.MinInstanceId || instanceId > ApiConstant.MaxInstanceId)
            {
                throw new ArgumentException(ApiMessages.API002);
            }
            _instanceId = instanceId;
        }
        public int GenerateUniqueId()
        {
            lock (_lock)
            {
                // Get the last 2 digits of the current timestamp (milliseconds)
                var timestampComponent = (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() % 100).ToString(ApiConstant.TwoDigitFormat);

                // Increment the sequence counter (reset if it exceeds 99)
                _sequenceCounter = (_sequenceCounter + 1) % 100;
                var sequenceComponent = _sequenceCounter.ToString(ApiConstant.TwoDigitFormat);

                // Generate a random 2-digit number (ensure it doesn't start with 0)
                var random = new Random();
                var randomComponent = random.Next(1, 100).ToString(ApiConstant.TwoDigitFormat);

                // Combine all components to form the 6-digit ID
                int uniqueId = Convert.ToInt32($"{_instanceId}{timestampComponent}{randomComponent}");

                return uniqueId; // This will always be a 6-digit string with NodeId as the first two digits
            }
        }
    }
}
