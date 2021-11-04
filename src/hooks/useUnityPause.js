import { useEffect, useState } from "react"

export const useUnityPause = () => {

  const [paused, setPaused] = useState(false);

  const unpause = () => setPaused(false);
  const pause = () => setPaused(true);

  const unpauseEvent = new CustomEvent('unity-unpause');
  const pauseEvent = new CustomEvent('unity-pause');

  useEffect(() => {
    window.addEventListener('unity-unpause', unpause, false);
    window.addEventListener('unity-pause', pause, false);
  }, [])

  return { paused, unpauseEvent, pauseEvent }
}