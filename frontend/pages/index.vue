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
    <div class="actions flex_r">
      <NuxtLink to="/cadastro">
        <button class="btn-1 button-1">Inscrever-se</button>
      </NuxtLink>
      <NuxtLink to="/login">
        <button class="link-1 button-1" data-content="Já sou usuário">
          Já sou usuário
        </button>
      </NuxtLink>
    </div>
  </div>
</template>

<script>
export default {}
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
}
.actions {
  margin-right: auto;
  margin-top: 50px;
  width: fit-content;
  align-items: center;
  gap: 20px;
}
</style>
