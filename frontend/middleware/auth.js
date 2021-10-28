

    export default async function ({ redirect, $axios, $cookies }) {
        await $axios.post('/v1/Auth/validate-token').catch(err => {
            console.log(err);
            $axios.get('/v1/Auth/revoke').then(response => {
                $cookies.set('userData', response.data);
                return redirect('/login');
            }).catch(err => {
                $cookies.get('userData') ? $cookies.remove('userData') : null;
                console.log(`revoke error ${err}`);
                return redirect('/login');
            });
        });
    }
