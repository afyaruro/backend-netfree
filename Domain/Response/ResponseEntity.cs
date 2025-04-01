
namespace Domain.Entity.Response
{
    public class ResponseEntity<E>
    {
        public string message { get; set; }
        public bool isError { get; set; }
        public E? entity { get; set; }
        public List<E>? listEntity { get; set; }
        public int totalPages { get; set; }
        public int totalRecords { get; set; }



        public ResponseEntity(string message, E entity)
        {
            this.message = message;
            this.entity = entity;
            this.isError = false;
        }

        public ResponseEntity(string message, List<E> listEntity)
        {
            this.message = message;
            this.listEntity = listEntity;
            this.isError = false;
        }

        public ResponseEntity(string message, bool isError)
        {
            this.message = message;
            this.isError = isError;
        }




    }
}