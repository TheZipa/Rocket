using System.Collections;
using Code.Infrastructure.ServiceContainer;
using UnityEngine;

namespace Code.Services.CoroutineRunner
{
    public interface ICoroutineRunner : IService
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}