import { Entity, PrimaryGeneratedColumn, Column, Index } from 'typeorm';

@Entity('default_index')
export class DefaultIndex {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column({ length: 250 })
  @Index()
  indexField: string;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}