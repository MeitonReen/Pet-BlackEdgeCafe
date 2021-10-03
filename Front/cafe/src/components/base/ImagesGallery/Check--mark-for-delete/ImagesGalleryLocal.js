
  
  let images = [];
  images.push("../images/1.jpg");
  images.push("../images/2.jpg");
  images.push("../images/3.jpg");
  images.push("../images/4.jpg");
  images.push("../images/5.jpg");
  let currentIndex = 0;

  ($ => {
    const indicator = $("<img>").
      attr("src", "../indicators/indicator.svg").
      addClass("images-gallery__indicator");

    const activeIndicator = $("<img>").
      attr("src", "../indicators/indicator.svg").
      addClass("images-gallery__indicator").
      addClass("images-gallery__indicator_active");
    const currentImage = $(".images-gallery__image");
    const indicatorsArea = $(".images-gallery__indicators");

    function setPreviousImage() {
      if (currentIndex > 0) {
        currentIndex -= 1;
        currentImage.attr("src", images[currentIndex]);
        refreshIndicators(indicatorsArea, currentIndex, images.length, indicator, activeIndicator);
      }
    }
  
    function setNextImage() {
      if (currentIndex < images.length - 1) {
        currentIndex += 1;
        currentImage.attr("src", images[currentIndex]);
        refreshIndicators(indicatorsArea, currentIndex, images.length, indicator, activeIndicator);
      }
    }
  
    currentImage.on("click", (e) => {
      if (e.pageX < e.target.width / 2) {
        setPreviousImage();
      } else {
        setNextImage();
      }
    })
  
    currentImage.attr("src", images[currentIndex]);
    refreshIndicators(indicatorsArea, currentIndex, images.length, indicator, activeIndicator);
  
    function refreshIndicators(indicatorsArea, currentIndex, length, indicator, activeIndicator) {
      indicatorsArea.empty();
      let outputIndicators = [];
  
      if (length > 0)
      {
        if (currentIndex == 0) {
          outputIndicators.push(activeIndicator.clone());
        } else {
          outputIndicators.push(indicator.clone());
        }
      }
  
      if (length > 1)
      {
        if ((currentIndex == length - 1) && (length == 2))
        {
          outputIndicators.push(activeIndicator.clone());
        } else if ((currentIndex > 0) && (currentIndex < length - 1)) {
          outputIndicators.push(activeIndicator.clone());
        } else {
          outputIndicators.push(indicator.clone());
        }
      }
  
      if (length > 2) {
        if (currentIndex == length - 1) {
          outputIndicators.push(activeIndicator.clone());
        } else {
          outputIndicators.push(indicator.clone());
        }
      }
      outputIndicators.forEach(indicator => indicatorsArea.append(indicator.clone()));
    }
  })(jQuery)
