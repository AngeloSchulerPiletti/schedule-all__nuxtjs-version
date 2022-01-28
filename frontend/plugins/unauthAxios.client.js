
export default ({app, $axios}, inject) => {
    const unauthAxios = $axios.create();
    unauthAxios.setToken(false);
    inject('unauthAxios', unauthAxios);
}