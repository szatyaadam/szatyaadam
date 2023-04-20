const urlBase = import.meta.env.VITE_APP_BASE_URL

const mealUrl = urlBase + "/meal"
const measureUrl = urlBase + "/measure"
const mealTypeUrl = urlBase + "/mealtype"
const userUrl = urlBase + "/user"
const favoriteUrl = urlBase + "/favorite"
const tokenUrl = urlBase + "/token"

export default {urlBase, mealUrl, measureUrl, mealTypeUrl, userUrl, favoriteUrl, tokenUrl}