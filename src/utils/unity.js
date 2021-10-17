import { UnityContext } from "react-unity-webgl";
import { join } from 'path'
import { serverStaticPath } from "./env";

export const getSceneUnityContext = (folderpath) => {

  const scenePath = join(serverStaticPath, folderpath)

  return new UnityContext({
    loaderUrl: `/static/scene_2/Build/TestRender.loader.js`,
    dataUrl: `/static/scene_2/Build/TestRender.data`,
    frameworkUrl: `/static/scene_2/Build/TestRender.framework.js`,
    codeUrl: `/static/scene_2/Build/TestRender.wasm`,
  });
}