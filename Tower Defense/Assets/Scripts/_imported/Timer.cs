
namespace SpaceShooter
{
    /// <summary>
    /// Класс таймера.
    /// </summary>
    public class Timer
    {
        private float m_currentTime;
        private float m_startTime;
        public bool IsFinished => m_currentTime <= 0;

        public Timer(float startTime)
        {
            Start(startTime);
        }

        public void Start(float startTime)
        {
            m_currentTime = startTime;

            m_startTime = startTime;
        }

        public void RemoveTime(float deltaTime)
        {
            if (m_currentTime <= 0) return;

            m_currentTime -= deltaTime;
        }

        public void Restart()
        {
            m_currentTime = m_startTime;
        }
    }
}