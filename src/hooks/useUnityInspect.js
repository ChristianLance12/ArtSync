import { useEffect, useState } from "react"

export const useUnityInspect = () => {

  const [inspectedImage, setInspectedImage] = useState(null);

  const inspectEvent = new CustomEvent('unity-inspect', {detail: "Any Object Here"});
  const uninspectEvent = new CustomEvent('unity-uninspect');

  useEffect(() => {
    window.addEventListener('unity-inspect', ({detail}) => setInspectedImage(detail), false);
    window.addEventListener('unity-uninspect', ({detail}) => setInspectedImage(detail), false);
  }, [])

  return { inspectedImage, inspectEvent, uninspectEvent }
}