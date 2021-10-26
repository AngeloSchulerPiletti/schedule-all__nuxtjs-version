


export default async function ({ redirect, $axios, $cookies }) {
    if ($cookies.get('userData')) {
        await $axios.post('/v1/Auth/validate-token').then(response => {
            return redirect('/');
        }).catch(async err => {
            $cookies.remove('userData');
            return redirect('/login');
        });
    }
}