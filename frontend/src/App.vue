<script setup>
// import { RouterLink, RouterView } from 'vue-router'
import Header from "./components/Header.vue";
import Footer from "./components/Footer.vue";
import Notification from './components/Notification.vue'
</script>

<template>
    <div class="app-container">
        <Header></Header>
        <main class="main-content">
            <RouterView />
        </main>
        <Footer></Footer>    
        <Notification />   
    </div>   
</template>

<style scoped>
.app-container {
    display: flex;
    flex-direction: column;
    min-height: 100vh;
}

.main-content {
    flex: 1; /* Đảm bảo phần nội dung có thể mở rộng */
    padding: 20px;
}
</style>
