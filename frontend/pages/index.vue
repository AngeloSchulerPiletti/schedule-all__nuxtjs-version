<template>
  <div class="inicio flex_c">
    <h1 class="title-1">SCHEDULE-ALL</h1>
    <p class="paragraph-1">
      Schedule-all é uma multi-plataforma de anotação, desenvolvimento e
      planificação de ideias. Fornecemos a você, gratuitamente, todas as
      ferramentas necessárias para que você organize não só seu trabalho, estudo
      ou tarefas, mas sim, toda a sua vida. Disponibilizamos a você um ambiente
      multi-plataforma com aplicativo iOS, Android, Windows e esse site.
    </p>
    <div class="actions flex_r" v-if="!$cookies.get('userData')">
      <NuxtLink to="/cadastro">
        <button class="btn-1 button-1">Inscrever-se</button>
      </NuxtLink>
      <NuxtLink to="/login">
        <button class="link-1 button-1" data-content="Já sou usuário">
          Já sou usuário
        </button>
      </NuxtLink>
    </div>
    <div class="actions flex_r" v-if="$cookies.get('userData')">
      <NuxtLink to="/schedule">
        <button class="btn-1 button-1">Meu Schedule</button>
      </NuxtLink>
    </div>
    <div class="logout_container flex_r" v-if="$cookies.get('userData')" @click="logout">
      <logout /> <span>Sair</span>
    </div>
  </div>
</template>

<script>
import Logout from '@/components/Logout.vue'

export default {
  methods:{
    async logout(){
        await this.$axios.get('/v1/Auth/revoke').catch(err => {`logout falhou no endpoint: ${err}`});
        this.$cookies.remove('userData');
        this.$router.go(0);
    },
  },
  components: {
    Logout,
  },
}
</script>

<style lang="scss" scoped>
.inicio {
  padding: 7vw 10vw;
  height: 100vh;
  box-sizing: border-box;

  background: linear-gradient(135deg, #e6e6e6 30%, #ffffff);

  h1 {
    margin-bottom: 20px;
  }
  p {
    width: 55vw;
  }
  .actions {
    margin-right: auto;
    margin-top: 50px;
    width: fit-content;
    align-items: center;
    gap: 20px;
  }
  .logout_container {
    cursor: pointer;
    position: absolute;
    bottom: 2vw;
    left: 2vw;
    align-items: center;
    gap: 7px;
    margin-top: auto;
    font-weight: 300;
    font-size: 16px;

    span{
      transition: transform 300ms;
    }
    svg{
      transition: opacity 300ms;
    }

    &:hover{
      span{
        transform: translateX(-100%);
      }
      svg{
        opacity: 0.1;
      }
    }
  }
}
</style>
