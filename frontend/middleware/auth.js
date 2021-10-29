

export default async function ({ redirect, $axios, $cookies }) {
    await $axios.post('/v1/Auth/validate-token').catch(err => {
        console.log(err);
        $cookies.get('userData') ? $cookies.remove('userData') : null;
        console.log(`revoke error ${err}`);
        return redirect('/login');
    });
}
