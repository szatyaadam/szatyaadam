<template>
    <div>
            Válasszon mértékegységet
        <select v-model="mertek"  >
            <option value="-1" disabled selected>Válasszon</option>
            <option value="Hossz_mertek">Hossz mérték</option>
            <option value="Terfogat">Térfogat</option>
            <option value="Tömeg">Tömeg</option>
            <option value="Ido">Idő</option>
            
        </select>
    </div>
    <div v-if="mertek=='Hossz_mertek'">
               
               A megadott Hosszmérték  <input type="number"  id="numbers" :value="adat1"  @change="setLength" placeholder="Adjon meg egy számot!">
               <select id="fromLength" @change="setLength">
                       <option value=1>mm</option>
                       <option value=2>cm</option>
                       <option value=3>dm</option>
                       <option value=4>m</option>
                    
                        
                   </select>
              
               <br>
         
               Az eredmény  {{adat2}}
                   <select id="toLength" @change="setLength">
                    <option value=1>mm</option>
                       <option value=2>cm</option>
                       <option value=3>dm</option>
                       <option value=4>m</option>
                  
                          
                   </select>
              <br>
        <img src="https://1.bp.blogspot.com/-Y3lp97gmRPE/X6hUMVlGlGI/AAAAAAAAwRU/hN0WDBFjdH0eS4KiDoFS5OREZDrvr1KDQCLcBGAsYHQ/s550/hossz%25C3%25BAs%25C3%25A1g.jpg" alt="centi-incs" >
                 
    
                 
       </div>
    
    <div v-if="mertek=='Terfogat'">
               
            A megadott Térfogat         <input type="number" id="numbers2" :value="adat1" @change="setVolume" placeholder="Adjon meg egy számot!">
                <select id="fromVolume" @change="setVolume">
                    <option value=1>ml-ben</option>
                    <option value=2 >cl-ben</option>
                    <option value=3 >dl-ben</option>
                    <option value=4>l-ben</option>      
                </select>
    
            <br>
            Az eredmény {{adat2}}
                <select id="toVolume" @change="setVolume">
                    <option value=1>ml</option>
                    <option value=2 >cl</option>
                    <option value=3 >dl</option>
                    <option value=4>l</option>           
                </select>
                <br>
              
                <br> 
                <img src="https://cms.sulinet.hu/get/d/ac20ea2b-f8b4-4d2c-b454-1043f85c2b47/1/9/b/Normal/5kepBT02_08.jpg" alt="Űrmérték"  >
    </div>
    
    
    <div v-if="mertek=='Tömeg'">
           A Megadott Tömeg  <input type="number"   id="numbers3" :value="adat1" @change="setweight" placeholder="Adjon meg egy számot!">
            <select id="fromWeight" @change="setweight">
                <option value=0>mg-ban</option>
                <option value=1 >g-ban</option>
                <option value=2>dkg-ban</option>
                <option value=3>kg-ban</option>
               
            </select>
            
              
                <br>
            Az Eredmény {{adat2}}
            <select id="toWeight" @change="setweight">
                <option value=0>mg-ban</option>
                <option value=1 >g-ban</option>
                <option value=2>dkg-ban</option>
                <option value=3>kg-ban</option>
            
            </select>
            <br>
            <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTPW5EbdlQosSdjsgHHY4ZiXaxUGsGnSV8ruqfCW8svbviEt7XF3WpvfOe6QQUj9DUqZA&usqp=CAU" alt="Súly-táblázat"  >
       
    </div>
    <div  v-if="mertek=='Ido'">
        Megadott Idő  <input type="number" id="numbers4" :value="adat1" @change="setTime"  placeholder="Adjon meg egy számot!">
        <select id="fromTime" @change="setTime"
        >
        
        <option value=1>Másodperc</option>
        <option value=2>Perc</option>
        <option value=3>Óra</option>
        <option value=4>Nap</option>     
    </select>
    
    <br>
    
    Az Eredmény  {{adat2}}
    <select id="toTime" @change="setTime"
    >
    <option value=1>Másodperc</option>
    <option value=2>Perc</option>
    <option value=3 >Óra</option>
    <option value=4>Nap</option>   
    </select>
    
        <br>
            <img src="https://slideplayer.hu/slide/11902630/67/images/17/Id%C5%91.jpg" alt="Idő-Táblázat"  >
    </div>
    
    </template>
    
    <script setup>
    
    import {ref}from "vue";
    
        const adat1=ref(0);
        const adat2=ref(0);
        const mertek=ref('')
        const Weight2=[1000,10,100,1000]
        const Time2=[60,60,60,24]
    
    
    function setVolume(e, v = +e.target.value) {
        adat1.value = numbers2.value
        adat2.value=(adat1.value/Math.pow(10,(toVolume.value-fromVolume.value)))
        // if (fromVolume.value>toVolume.value)
        // {
        //     adat2.value=(adat1.value*10)
        // }
        // else if (fromVolume.value<toVolume.value)
        // {
        //     adat2.value=(adat1.value/Math.pow(10,(toVolume.value-fromVolume.value)))
        // }
        // else adat2.value=v
    }
    function setLength(e, v = +e.target.value) {
        adat1.value = numbers.value
        adat2.value=(adat1.value/Math.pow(10,(toLength.value-fromLength.value)))   
    }
    function setweight(e, v = +e.target.value) {
        v=numbers3.value
        adat1.value = v
    if (toWeight.value<fromWeight.value)
    {   adat2.value=0
        for(let i=fromWeight.value-1;i>=toWeight.value;i--)
        {
           
            v=v*Weight2[i];
           adat2.value=v
    
        }
           
    }
            else if (toWeight.value>fromWeight.value)
            {
                adat2.value=0
                for(let i=fromWeight.value;i<toWeight.value;i++)
                {
                    v=v/Weight2[i];
                    adat2.value=v
                } 
            }
            else adat2.value=adat1.value
        
    
    
    
        }
     
      
     function setTime(e,v=+e.target.value)
     {
        v=numbers4.value
        adat1.value = v
            if (toTime.value<fromTime.value)
            {   adat2.value=0
                for(let i=fromTime.value-1;i>=toTime.value;i--)
                {
                
                    v=v*Time2[i];
                adat2.value=v
    
                
                }
                
            }
            else if (toTime.value>fromTime.value)
            {
                adat2.value=0
                for(let i=fromTime.value;i<toTime.value;i++)
                {
                    v=v/Time2[i];
                    adat2.value=v
                }
            }
            else adat2.value=adat1.value
        
    
    
    
        }
    
    </script>
    
    <style scoped>
    button {
                margin-top:20px;
                margin-left: 200px;
            }
        img{
           width: 400px; 
            border: black solid 2px;
            box-shadow: 2px 2px blue;
            margin:100px;
            
            
           
        }
    </style>