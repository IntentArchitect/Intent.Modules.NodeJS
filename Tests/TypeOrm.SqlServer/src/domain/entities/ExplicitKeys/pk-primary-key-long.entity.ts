import { Entity, PrimaryGeneratedColumn, Column } from 'typeorm';

@Entity('pk_primary_key_long')
export class PK_PrimaryKeyLong {
  @PrimaryGeneratedColumn()
  primaryKeyLong: number;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}