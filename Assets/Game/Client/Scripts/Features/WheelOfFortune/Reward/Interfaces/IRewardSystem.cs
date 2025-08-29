using System;
using System.Threading.Tasks;

namespace Game.Client.Scripts.Features.WheelOfFortune.Reward
{
    public interface IRewardSystem : IDisposable
    {
        event Action OnRewardAnimationCompleted;
        Task StartRewardAnimationAsync();
    }
}