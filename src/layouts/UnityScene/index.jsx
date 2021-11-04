import Unity, { UnityContext } from "react-unity-webgl";

const UnityScene = ({ currentScene, onLoad }) => {
  
  const unityContext = new UnityContext(currentScene.context);
  unityContext.on("loaded", () => onLoad(unityContext));

  return (
    <Unity
      unityContext={unityContext}
      onLoad={() => console.log("ON LOAD")}
      style={{
        height: 600,
        width: 950,
        border: "2px solid black",
        background: "grey",
      }}
    />
  );
};

export default UnityScene;
