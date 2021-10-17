import { useScenes } from "../../hooks/useScenes";
import Loading from "../../components/Loading";
import { toHumanReadable } from "../../utils/string";

import "./style.scss";

const ScenesList = ({ onSceneSelect, currentScene }) => {
  const { loading, scenes } = useScenes();

  return (
    <Loading loading={loading}>
      <div className="scenes-list">
        {scenes &&
          scenes.map((scene) => {
            const selected = currentScene && scene.id === currentScene.id;
            return (
              <button onClick={() => onSceneSelect(scene)} key={scene.id}>
                {selected && "*"}
                {toHumanReadable(scene.name)}
              </button>
            );
          })}
      </div>
    </Loading>
  );
};

export default ScenesList;
