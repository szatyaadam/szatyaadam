<template>
    <div class="d-flex justify-content-center w-100 mt-4">
        <form class="form text-center d-flex flex-column flex-lg-row justify-content-center w-75" 
              @submit.prevent="modifyUser">
            <div class=" w-100">
                <h1>Profil</h1>
                <!-- USERNAME -->
                <label>Név</label>
                <div class="d-flex flex-lg-row justify-content-center align-items-center gap-1 my-1">
                    <input v-if="editName"
                                 type="text" v-model="newUser.userName">
                    <span v-else
                          class="d-flex justify-content-between align-items-center">
                        {{ user.user.userName }}
                    </span>
                    <!-- EDIT BUTTON -->
                    <button v-if="!editName" 
                            type="button" 
                            @click.prevent="editName = !editName">
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 640 512"><path d="M224 256A128 128 0 1 0 224 0a128 128 0 1 0 0 256zm-45.7 48C79.8 304 0 383.8 0 482.3C0 498.7 13.3 512 29.7 512H322.8c-3.1-8.8-3.7-18.4-1.4-27.8l15-60.1c2.8-11.3 8.6-21.5 16.8-29.7l40.3-40.3c-32.1-31-75.7-50.1-123.9-50.1H178.3zm435.5-68.3c-15.6-15.6-40.9-15.6-56.6 0l-29.4 29.4 71 71 29.4-29.4c15.6-15.6 15.6-40.9 0-56.6l-14.4-14.4zM375.9 417c-4.1 4.1-7 9.2-8.4 14.9l-15 60.1c-1.4 5.5 .2 11.2 4.2 15.2s9.7 5.6 15.2 4.2l60.1-15c5.6-1.4 10.8-4.3 14.9-8.4L576.1 358.7l-71-71L375.9 417z"/></svg>
                    </button>
                    <div v-else>
                        <!-- CLOSE EDIT BUTTON-->
                        <button type="button" 
                                @click.prevent="{editName = !editName};{newUser.userName = ''}">
                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><path d="M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM175 175c9.4-9.4 24.6-9.4 33.9 0l47 47 47-47c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9l-47 47 47 47c9.4 9.4 9.4 24.6 0 33.9s-24.6 9.4-33.9 0l-47-47-47 47c-9.4 9.4-24.6 9.4-33.9 0s-9.4-24.6 0-33.9l47-47-47-47c-9.4-9.4-9.4-24.6 0-33.9z"/></svg>
                        </button>
                        <!-- SAVE BUTTON -->
                        <button v-if="newUser.userName.length >= 3"
                                type="submit">
                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><path d="M243.8 339.8C232.9 350.7 215.1 350.7 204.2 339.8L140.2 275.8C129.3 264.9 129.3 247.1 140.2 236.2C151.1 225.3 168.9 225.3 179.8 236.2L224 280.4L332.2 172.2C343.1 161.3 360.9 161.3 371.8 172.2C382.7 183.1 382.7 200.9 371.8 211.8L243.8 339.8zM512 256C512 397.4 397.4 512 256 512C114.6 512 0 397.4 0 256C0 114.6 114.6 0 256 0C397.4 0 512 114.6 512 256zM256 48C141.1 48 48 141.1 48 256C48 370.9 141.1 464 256 464C370.9 464 464 370.9 464 256C464 141.1 370.9 48 256 48z"/></svg>
                        </button>
                    </div>
                </div>
                <div class="message_space">
                    <!-- ERROR MESSAGE -->
                    <p class="error" v-if="newUser.userName.length < 3 && 
                                        newUser.userName.length != 0">Minimum 3 karakter!</p>
                </div>
                <!-- EMAIL -->
                <label>Email</label>
                <div class="d-flex flex-lg-row justify-content-center align-items-center gap-1 my-1">
                    <input v-if="editEmail" 
                                 type="text" 
                                 v-model="newUser.email">
                    <span v-else 
                          class="d-flex justify-content-between align-items-center">
                        {{ user.user.email }}
                    </span>
                    <!-- EDIT BUTTON -->
                    <button v-if="!editEmail" 
                            type="button" 
                            @click.prevent="editEmail = !editEmail">
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 640 512"><path d="M224 256A128 128 0 1 0 224 0a128 128 0 1 0 0 256zm-45.7 48C79.8 304 0 383.8 0 482.3C0 498.7 13.3 512 29.7 512H322.8c-3.1-8.8-3.7-18.4-1.4-27.8l15-60.1c2.8-11.3 8.6-21.5 16.8-29.7l40.3-40.3c-32.1-31-75.7-50.1-123.9-50.1H178.3zm435.5-68.3c-15.6-15.6-40.9-15.6-56.6 0l-29.4 29.4 71 71 29.4-29.4c15.6-15.6 15.6-40.9 0-56.6l-14.4-14.4zM375.9 417c-4.1 4.1-7 9.2-8.4 14.9l-15 60.1c-1.4 5.5 .2 11.2 4.2 15.2s9.7 5.6 15.2 4.2l60.1-15c5.6-1.4 10.8-4.3 14.9-8.4L576.1 358.7l-71-71L375.9 417z"/></svg>
                    </button>
                    <div v-else>
                        <!-- CLOSE EDIT BUTTON -->
                        <button type="button" 
                                @click.prevent="{editEmail = !editEmail};{newUser.email = ''}">
                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><!--! Font Awesome Pro 6.3.0 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. --><path d="M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM175 175c9.4-9.4 24.6-9.4 33.9 0l47 47 47-47c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9l-47 47 47 47c9.4 9.4 9.4 24.6 0 33.9s-24.6 9.4-33.9 0l-47-47-47 47c-9.4 9.4-24.6 9.4-33.9 0s-9.4-24.6 0-33.9l47-47-47-47c-9.4-9.4-9.4-24.6 0-33.9z"/></svg>
                        </button>
                        <!-- SAVE BUTTON -->
                        <button v-if="regex.test(newUser.email)"
                                type="submit">
                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><path d="M243.8 339.8C232.9 350.7 215.1 350.7 204.2 339.8L140.2 275.8C129.3 264.9 129.3 247.1 140.2 236.2C151.1 225.3 168.9 225.3 179.8 236.2L224 280.4L332.2 172.2C343.1 161.3 360.9 161.3 371.8 172.2C382.7 183.1 382.7 200.9 371.8 211.8L243.8 339.8zM512 256C512 397.4 397.4 512 256 512C114.6 512 0 397.4 0 256C0 114.6 114.6 0 256 0C397.4 0 512 114.6 512 256zM256 48C141.1 48 48 141.1 48 256C48 370.9 141.1 464 256 464C370.9 464 464 370.9 464 256C464 141.1 370.9 48 256 48z"/></svg>
                        </button>
                    </div>
                </div>
                <div class="message_space">
                    <!-- ERROR MESSAGE -->
                    <p class="error" v-if="!regex.test(newUser.email) &&
                                        newUser.email.length != 0">Email cím formátuma nem megfelelő!</p>
                </div>
                <!-- PASSWORD -->
                <div class="d-flex flex-column flex-lg-row justify-content-center align-items-center gap-3 my-2">
                    <label v-if="!editPassword">Jelszó</label>
                    <div v-if="editPassword">

                        <p>Jelszó</p>
                        <input type="password" 
                               v-model="newUser.password">

                        <p>Jelszó megerősítése</p>
                        <input type="password" 
                               v-model="passwordConfirm">
                    </div>
                    <!-- EDIT BUTTON -->
                    <button v-if="!editPassword" 
                            type="button" 
                            @click.prevent="editPassword = !editPassword">
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 640 512"><path d="M224 256A128 128 0 1 0 224 0a128 128 0 1 0 0 256zm-45.7 48C79.8 304 0 383.8 0 482.3C0 498.7 13.3 512 29.7 512H322.8c-3.1-8.8-3.7-18.4-1.4-27.8l15-60.1c2.8-11.3 8.6-21.5 16.8-29.7l40.3-40.3c-32.1-31-75.7-50.1-123.9-50.1H178.3zm435.5-68.3c-15.6-15.6-40.9-15.6-56.6 0l-29.4 29.4 71 71 29.4-29.4c15.6-15.6 15.6-40.9 0-56.6l-14.4-14.4zM375.9 417c-4.1 4.1-7 9.2-8.4 14.9l-15 60.1c-1.4 5.5 .2 11.2 4.2 15.2s9.7 5.6 15.2 4.2l60.1-15c5.6-1.4 10.8-4.3 14.9-8.4L576.1 358.7l-71-71L375.9 417z"/></svg>
                    </button>
                    <div v-else>
                        <!-- CLOSE EDIT BUTTON -->
                        <button type="button" 
                                @click.prevent="{editPassword = !editPassword};{newUser.password = ''}">
                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><path d="M256 512c141.4 0 256-114.6 256-256S397.4 0 256 0S0 114.6 0 256S114.6 512 256 512zM175 175c9.4-9.4 24.6-9.4 33.9 0l47 47 47-47c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9l-47 47 47 47c9.4 9.4 9.4 24.6 0 33.9s-24.6 9.4-33.9 0l-47-47-47 47c-9.4 9.4-24.6 9.4-33.9 0s-9.4-24.6 0-33.9l47-47-47-47c-9.4-9.4-9.4-24.6 0-33.9z"/></svg>
                        </button>
                        <!-- SAVE BUTTON -->
                        <button v-if="newUser.password == passwordConfirm &&
                                      newUser.password.length > 7" 
                                      type="submit">
                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512"><path d="M243.8 339.8C232.9 350.7 215.1 350.7 204.2 339.8L140.2 275.8C129.3 264.9 129.3 247.1 140.2 236.2C151.1 225.3 168.9 225.3 179.8 236.2L224 280.4L332.2 172.2C343.1 161.3 360.9 161.3 371.8 172.2C382.7 183.1 382.7 200.9 371.8 211.8L243.8 339.8zM512 256C512 397.4 397.4 512 256 512C114.6 512 0 397.4 0 256C0 114.6 114.6 0 256 0C397.4 0 512 114.6 512 256zM256 48C141.1 48 48 141.1 48 256C48 370.9 141.1 464 256 464C370.9 464 464 370.9 464 256C464 141.1 370.9 48 256 48z"/></svg>
                        </button>
                    </div>
                </div>
                <!-- NOTIFY USER -->
                <div>
                    <!-- PASSWORD CONFIRM ERROR MESSAGE -->
                    <p v-if="newUser.password != passwordConfirm" 
                            class="error">A jelszó nem egyezik!</p>
                    <!-- PASSWORD LENGHT CHECK -->
                    <p v-if="newUser.password.length <= 7 &&
                             newUser.password.length != 0" 
                             class="error">Minimum 8 karakter hosszúnak kell lenni.</p>
                    <!--SERVER ERROR MESSAGE -->
                    <p v-if="user.errorMessage" 
                             class="error">{{ user.errorMessage }}</p>
                    <!-- NOTIFY MESSAGE -->
                    <p v-if="user.notifyMessage" 
                             class="success" >{{ user.notifyMessage }}</p>
                </div>
            </div>
        </form>
    </div>
</template>

<script setup>
import { useUserStore } from '../stores/user';
import { ref, watch, reactive } from 'vue';
import axios from 'axios';
import base from '../service/base';

const user = useUserStore()
const newUser = reactive({
    userName: '',
    email: '',
    password: ''
})

// //source : https://uibakery.io/regex-library/email
const regex = /^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$/


const editName = ref(false)
const editEmail = ref(false)
const editPassword = ref(false)
const passwordConfirm = ref('')

watch(editPassword, () => {
    if(editPassword){
        newUser.password = ''
        passwordConfirm.value = ''
    }
})

//USER MODIFY
function modifyUser(){
    axios.put(base.userUrl + '/modify',{
      userName : newUser.userName,
      email: newUser.email,
      password: newUser.password
    }, user.config())
    .then(res => {
        if(newUser.userName != ''){
            user.user.userName = newUser.userName
        }

        if (newUser.email != ''){
            user.user.email = newUser.email
        }

        editName.value = false
        editEmail.value = false
        editPassword.value = false
        user.createNotifyMessage(res.data)
    })
    .catch(err => {
        newUser.userName = user.user.userName
        newUser.email = user.user.email
        editName.value = false
        editEmail.value = false
        editPassword.value = false
        user.createErrorMessage(err.response.data);
    })
}

</script>

<style scoped>
form{
    background-color: antiquewhite;
    min-height: 350px;
    min-width: 300px;
    border: 10px solid white;
    border-radius: 5px;
    padding: 20px;
}

button:not(.btn){
    background-color: transparent;
    border-color: transparent;
}

svg{
    height: 20px;
    aspect-ratio: 1;
}

span{
    width: 80%;
    height: 30px;
    max-width: 350px;
    background-color: rgba(188, 194, 17, 0.156);
    border-radius: 20px;
    text-indent: 20px;
}

input{
    height: 30px;
    width: 75%;
    max-width: 330px;
    min-width: 200px;
    background-color: rgba(222, 238, 251, 0.276);
    border-radius: 20px;
    text-indent: 10px;
}

#profile_picture{
    min-width: 200px;
    max-width: max-content;
    height: 300px;
    background-image: url("https://letsenhance.io/static/334225cab5be263aad8e3894809594ce/75c5a/MainAfter.jpg");
    background-size: cover;
    background-position: center;
}

.message_space{
    height: 20px;
}
</style>
