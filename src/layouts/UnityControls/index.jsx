import UploadsList from "../../layouts/UploadsList";
import { isEmpty } from "lodash";
import { useMemo } from "react";
import { useUnityPause } from "../../hooks/useUnityPause";
import { useUnityInspect } from "../../hooks/useUnityInspect";

import "./style.scss";

const UnityControls = ({
  currentUnityContext,
  setCurrentUnityContext,
  currentScene,
  setCurrentScene,
}) => {

  console.log(currentUnityContext)

  const { paused, unpauseEvent, pauseEvent } = useUnityPause();
  const { inspectedImage, inspectEvent, uninspectEvent } = useUnityInspect();
  
  const exit = () => {
    setCurrentUnityContext(null)
    setCurrentScene(null);
  }

  const showInspectionPanel = useMemo(() => 
    !isEmpty(inspectedImage) && !paused,
    [inspectedImage, paused]);
  
  if ( isEmpty(currentUnityContext) ) return null;

  return (
    <div id="unity-controls">

      <div id="panel-paused" className={paused ? "visible" : "invisible"}>
        <h3>PAUSED PANEL</h3>
        <button onClick={exit}>Exit</button>
      </div>

      <div id="panel-inspect" className={showInspectionPanel ? "visible" : "invisible"}>
        <h3>INSPECTION PANEL</h3>
        <p>Inspected Image: {JSON.stringify(inspectedImage)}</p>
      </div>

      {/* NOTE: for testing only: remove */}
      <div id="panel-test">
        <button onClick={() => window.dispatchEvent(unpauseEvent)}>UNPAUSE</button>
        <button onClick={() => window.dispatchEvent(pauseEvent)}>PAUSE</button>

        <hr />

        <button onClick={() => window.dispatchEvent(inspectEvent)}>INSPECT</button>
        <button onClick={() => window.dispatchEvent(uninspectEvent)}>UNINSPECT</button>

        <hr />

        <button onClick={() => {

          const { unityInstance } = currentUnityContext;
          const data = JSON.stringify({
            frame: 1,
            spawn: 1, 
            url: "/static_content/uploads/van-gogh-1.jpg"
          })

          // const data = [1, 1, "/static_content/uploads/van-gogh-1.jpg"]

          unityInstance.SendMessage("GameController", "LoadArt", data)

        }}>TEST LOAD</button>
      </div>


    </div>
  );
};

export default UnityControls;
