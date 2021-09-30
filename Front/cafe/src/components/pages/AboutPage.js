import "./AboutPage.css"
import { generateImagesUrls } from "../../shared-funcs";

import DefaultPageWithMenu from "../base/DefaultPageWithMenu"
import ImagesGallery from "../base/ImagesGallery";

export default function AboutPage(props) {
  //#region PrepareJSX
  const body = (
    <div className="about-page__images-gallery">
      <ImagesGallery imagesUrls={Object.values(imagesModulesUrls)} />
    </div>
  );
  //#endregion
  return (
    <div className="about-page">
      <DefaultPageWithMenu body={body} isLogin={props.isLogin}
        setIsLogin={props.setIsLogin} />
    </div>
  );
}
//#region Other, not using state
const imagesModulesUrls = generateImagesUrls(
  require.context('../../images/images-gallery',
    false, /\.(png|jpe?g|svg)$/));
//#endregion