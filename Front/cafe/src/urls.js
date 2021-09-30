
import urls from "./urls.json"
import { ApiClient } from "./api/src/index"

urls.serverUrl = (new ApiClient()).hostSettings()[0].url;

export default urls;