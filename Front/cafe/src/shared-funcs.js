export function wordEndingOnNumber(number) {
  if (number >= 20) {
    number = number % 10;
  }
  if (number === 1) return "о";
  if (number >= 2 && number <= 4) return "а";
  return "";
}
export function dotsToCommas(obj) {
  return obj?.toString().replace(".", ",");
}
export function random(min, max) {
  return min + Math.random() * (max - min);
}

export function compareByKeyDefault(reactElOne, reactElTwo) {
  if (reactElOne.key < reactElTwo.key) return -1;
  if (reactElOne.key > reactElTwo.key) return 1;
  if (reactElOne.key === reactElTwo.key) return 0;
}

export function generateImagesUrls(r) {
  let dictImages = {};
  r.keys().forEach(item => dictImages[item
    .substring(0, item.lastIndexOf("."))
    .replace("./", "")] = r(item).default);
  return dictImages;
}
export function cacheImages(...urls) {
  const images = urls.map(imageUrl => {
    const image = new Image();
    image.src = imageUrl;
  });
}