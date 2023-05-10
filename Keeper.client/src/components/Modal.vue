<template>
  <div class="modal fade" tabindex="-1" aria-labelledby="modalTitleId" aria-hidden="true">
    <div class="modal-dialog" :class="size">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" :id="titleId">
            <slot name="header">
              You forgot the header!
            </slot>
          </h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <slot name="body">
            You forgot the body!
          </slot>
        </div>
        <!-- <div class="modal-footer">
          <button type="submit" class="btn btn-dark">Create</button>
        </div> -->
      </div>
    </div>
  </div>
</template>


<script setup>
const props = defineProps({ size: { type: String, default: 'modal-md' } }, { id: { type: String, default: 'modalForm' } })
</script>


<style lang="scss" scoped>
.modal-content {
  background-color: rgba(249, 246, 250, 1)
}

.modal-header {
  border-bottom: none;
}

.modal-title {
  color: rgba(99, 110, 114, 1)
}

.modal-footer {
  border-top: none;
}
</style>